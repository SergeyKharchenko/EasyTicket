import React from 'react';

export default class EmailInput extends React.Component {
    constructor(props) {
        super(props);
        this.id = props.id || window.easyTicket.utils.generateId();
    }

    render() {
        return (
            <div>
                <i className="material-icons prefix">email</i>
                <input id={this.id} type="text" className="validate" />
                <label htmlFor={this.id}>{this.props.label}</label>
            </div>
        );
    }
}