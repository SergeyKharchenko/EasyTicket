import React from 'react';
import 'materialize-css';
import EasyTicketClient from './../infrastructure/easyTicketClient'
import Error from './../infrastructure/error';

export default class StationSelect extends React.Component {
    constructor(props) {
        super(props);
        this.minLength = 3;
        this.inputId = window.easyTicket.utils.generateId();
        this._easyTicketClient = new EasyTicketClient();

        this.state = {
            loading: false,
            opened: false,
            selectedStation: null,
            items: [],
            error: null
        }
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
    generateErrorHtml(error) {
        return error ? <Error text={`${error.status}: ${error.statusText}`}/> : "";
    }
    render() {
        var itemsHtml = this.getItemsHtml(),
            error = window.easyTicket.utils.generateErrorHtml(this.state.error);

        return (
            <div className="station-select" id={this.inputId}>
                <div className={'input-field' + (this.state.loading ? ' loading' : '')}>
                    <input id={this.inputId + "-input"} type="text" className='validate' onChange={this.onChange.bind(this)}/>
                    <label htmlFor={this.inputId + "-input"}>{this.props.label}</label>
                </div>
                {itemsHtml}
                {error}
            </div>
        );
    }

    getItemsHtml() {
        var items = [],
            itemsHtml = '';
        if (this.state.opened) {
            for (var i in this.state.items) {
                var item = <StationSelectItem key={i} data={this.state.items[i]} onClick={this.onItemClick.bind(this)}/>;
                items.push(item);
            }
            if (items.length) {
                itemsHtml = <div className='collection'>{items}</div>;
            }
        }
        return itemsHtml;
    }

    onChange(e) {
        var value = e.target.value;
        if (value.length < this.minLength) {
            this.setState({ loading: false, opened: false });
            return;
        }
        this.setState({ loading: true });
        var that = this;
        this._easyTicketClient.getStations(value,
            data => {
                that.setState({
                    loading: false, 
                    items: data.stations,
                    opened: true
                });
            },
            error => {
                that.setState({ loading: false, error: error});
            });
    }

    onItemClick(station) {
        $(`#${this.inputId}-input`).val(station.title);
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
            <a className="collection-item" onClick={this.onClick.bind(this)}>{this.props.data.title}</a>
        );
    }

    onClick() {
        this.props.onClick(this.props.data);
    }
}