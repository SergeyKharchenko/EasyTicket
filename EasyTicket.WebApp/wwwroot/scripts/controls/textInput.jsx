import React from 'react';

export default class TextInput extends React.Component {
    constructor(props) {
        super(props);
        this.id = props.id || window.easyTicket.utils.generateId();
    }

    render() {
        var iconHtml = this.props.icon ? <i className="material-icons prefix">{this.props.icon}</i> : "";
        return (
            <div>
                {iconHtml}
                <input id={this.id} type="text" className="validate" />
                <label htmlFor={this.id}>{this.props.label}</label>
            </div>
        );
    }
}