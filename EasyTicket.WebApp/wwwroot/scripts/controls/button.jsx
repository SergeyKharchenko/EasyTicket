import React from 'react';

export default class Button extends React.Component {
    constructor(props) {
        super(props);
        this.id = props.id || window.easyTicket.utils.generateId();
    }

    render() {
        var iconHtml = this.props.icon ? <i className="material-icons right">{this.props.icon}</i> : "";
        return (
            <a className={"waves-effect waves-light btn" + (this.props.disabled ? " disabled": "")}
                onClick={this.onClick.bind(this)}>
                {iconHtml}
                {this.props.text}
            </a>
        );
    }

    onClick(e) {
        this.props.onClick(e);
    }
}