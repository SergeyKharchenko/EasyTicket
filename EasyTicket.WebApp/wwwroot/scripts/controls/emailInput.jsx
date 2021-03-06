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
                <input id={this.id} type="email" className="validate" onChange={this.onChange.bind(this)} />
                <label htmlFor={this.id} data-error="Неверный формат">{this.props.label}</label>
            </div>
        );
    }

    onChange(e) {
        if (this.props.onChange) {
            this.props.onChange(e.target.value);
        }
    }
}