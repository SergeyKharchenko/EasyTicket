import React from 'react';
import 'materialize-css';
import UzClient from './uzClient'

export default class StationSelect extends React.Component {
    constructor(props) {
        super(props);
        this.minLength = 3;
        this.inputId = this.generateId();
        this._uzClient = new UzClient();

        this.state = {
            loading: false,
            opened: false,
            selectedStation: null,
            items: []
        }
    }

    generateId() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
            c => {
                var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
                return v.toString(16);
            });
    }

    componentDidMount() {
        var that = this;
        $(document).on("click",
            e => {
                var stationSelect = $('#' + that.inputId).get(0);
                if (!stationSelect.contains(e.target)) {
                    that.setState({opened: false});
                }
            });
    }

    render() {
        var itemsHtml = this.getItemsHtml();
        return (
            <div className="station-select" id={this.inputId}>
                <div className={'input-field' + (this.state.loading ? ' loading' : '')}>
                    <input id={this.inputId + "-input"} type="text" className='validate' onChange={this.onChange.bind(this)}/>
                    <label htmlFor={this.inputId + "-input"}>{this.props.label}</label>
                </div>
                {itemsHtml}
            </div>
        );
    }

    getItemsHtml() {
        var items = [],
            itemsHtml = '';
        if (this.state.opened) {
            for (var i in this.state.items) {
                var item = <li key={i}>
                               <StationSelectItem data={this.state.items[i]} onClick={this.onItemClick.bind(this)}/>
                           </li>;
                items.push(item);
            }
            if (items.length) {
                itemsHtml = <ul>{items}</ul>;
            }
        }
        return itemsHtml;
    }

    onChange(e) {
        var value = e.target.value;
        if (value.length < this.minLength) {
            this.setState({loading: false});
            return;
        }
        this.setState({ loading: true });
        var that = this;
        this._uzClient.getStations(value,
            data => {
                that.setState({
                    loading: false,
                    items: data.stations,
                    opened: true
                });
            },
            error => {
                debugger;
            });
    }

    onItemClick(station) {
        $('#' + this.inputId + "-input").val(station.title);
        this.setState({opened: false});
        this.props.onStationSelected(station);
    }
}

class StationSelectItem extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div onClick={this.onClick.bind(this)}>
                {this.props.data.title}
            </div>
        );
    }

    onClick() {
        this.props.onClick(this.props.data);
    }
}