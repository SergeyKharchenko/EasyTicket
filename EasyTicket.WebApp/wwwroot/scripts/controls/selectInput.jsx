import React from 'react';
import 'materialize-css';

export default class GroupSelectInput extends React.Component {
    constructor(props) {
        super(props);
        this.id = props.id || window.easyTicket.utils.generateId();
    }

    componentDidMount() {
        this.initializeSelect();
    }

    componentWillUpdate() {
        var $select = $(`#${this.id}`);
        $select.material_select('destroy');
        $select.prop('selectedIndex', 0);
    }

    componentDidUpdate() {
        this.initializeSelect();
    }

    initializeSelect() {
        $(`#${this.id}`).on('change', this.onChange.bind(this)).material_select();
    }

    render() {
        var optionsHtml = this.props.options.map(option => {
            return <option key={option.value} value={option.value}>{option.text}</option>
        });
        var placeholderHtml = this.props.placeholder ? <option value="" disabled>{this.props.placeholder}</option> : "";
        var iconHtml = this.props.icon ? <i className="material-icons prefix">{this.props.icon}</i> : "";
        return (
            <div>
                {iconHtml}
                <select id={this.id} { ...(this.props.multiple && { multiple: true } )}>
                    {placeholderHtml}
                    {optionsHtml}
                </select>
                <label>{this.props.label}</label>
            </div>
        );
    }

    onChange(e) {
        if (!this.props.onChange) {
            return;
        }
        var value = $(e.target).val();
        this.props.onChange(value);
    }
}