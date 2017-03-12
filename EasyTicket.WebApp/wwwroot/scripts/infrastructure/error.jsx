import React from 'react';
import 'materialize-css';

export default class Error extends React.Component {
    constructor(props) {
        super(props);
        this.id = window.easyTicket.utils.generateId();
    }

    componentDidMount() {
        this.showError();
    }

    componentDidUpdate() {
        this.showError();
    }

    showError() {
        $(`#${this.id}`).modal().modal('open');
    }

    render() {
        return (
            <div id={this.id} className="modal">
                <div className="modal-content">
                    <h4>An error has occurred</h4>
                    <p className="error-message">{this.props.text}</p>
                </div>
                <div className="modal-footer">
                    <button href="" className="modal-action modal-close waves-effect waves-green btn-flat">Ok</button>
                </div>
            </div>
        );
    }
}