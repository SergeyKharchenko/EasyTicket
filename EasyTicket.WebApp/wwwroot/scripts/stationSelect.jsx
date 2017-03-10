import React from 'react';
import 'materialize-css';

export default class StationSelect extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            loading: false
        }

        this.minLength = 3;
    }

    componentDidMount() {
       
    }

    render() {
        return (
            <div className={"input-field" + (this.state.loading ? ' loading' : '')}>
                <input id="stationFrom" type="text" className='validate' onChange={this.onChange.bind(this)}/>            
                <label htmlFor="stationFrom">{this.props.label}</label>
            </div>
        )
    }

    onChange(e) {
        var value = e.target.value;
        if (value.length < this.minLength) {
            this.setState({loading: false});
            return;
        }
        this.setState({loading: true});
    }
}