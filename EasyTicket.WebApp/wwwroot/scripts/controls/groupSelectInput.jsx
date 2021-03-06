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
        $select.off('change').material_select('destroy');
        //$select.prop('selectedIndex', 0);
    }

    componentDidUpdate() {
        this.initializeSelect();
    }

    initializeSelect() {
        $(`#${this.id}`).on('change', this.onChange.bind(this)).material_select();
    }

    render() {
        var groupsHtml = this.props.groups.map(group => {
            var header = group.header;
            var options = group.options.map(option => {
                return <option key={option.value} value={option.value}>{option.text}</option>
            })
            return <optgroup key={header} label={header}>{options}</optgroup>
        }); 
        var iconHtml = this.props.icon ? <i className="material-icons prefix">{this.props.icon}</i> : "";
        return (
            <div>
                {iconHtml}
                <select id={this.id}>
                    {groupsHtml}
                </select>
                <label>{this.props.label}</label>
            </div>
        );
    }

    onChange(e) {
        if (!this.props.onChange) {
            return;
        }
        var value = e.target.value;
        this.props.onChange(value);
    }
}