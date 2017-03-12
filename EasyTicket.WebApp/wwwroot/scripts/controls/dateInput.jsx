import React from 'react';
import 'materialize-css';

export default class DateInput extends React.Component {
    constructor(props) {
        super(props);
        this.id = props.id || window.easyTicket.utils.generateId();
    }

    componentDidMount() {
        $(`#${this.id}`).pickadate({
            labelMonthNext: 'Следующий месяц',
            labelMonthPrev: 'Предыдущий месяц',
            labelMonthSelect: 'Выберите месяц',
            labelYearSelect: 'Выбирите год',
            monthsFull: [ 'Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь' ],
            monthsShort: [ 'Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн', 'Июл', 'Авг', 'Сен', 'Окт', 'Ноя', 'Дек' ],
            weekdaysFull: [ 'Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота' ],
            weekdaysShort: [ 'Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб' ],
            weekdaysLetter: [ 'Вс', 'Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб' ],
            today: 'Сегодня',
            clear: 'Очистить',
            close: 'Закрыть',
            firstDay: 1,

            selectMonths: true, // Creates a dropdown to control month
        });
    }

    render() {
        return (
            <div>
                <i className="material-icons prefix">schedule</i>
                <input id={this.id} type="date" className="datepicker" />
                <label htmlFor={this.id}>{this.props.label}</label>
            </div>
        );
    }
}