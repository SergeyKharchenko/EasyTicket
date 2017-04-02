import React from 'react';
import 'jquery.cookie';

import Toast from './../infrastructure/toast';

import EasyTicketClient from './../infrastructure/easyTicketClient';

import Loading from './../controls/loading';

export default class ReservationView extends React.Component {
    constructor(props) {
        super(props);

        this._easyTicketClient = new EasyTicketClient();

        this.state = {
            loading: true,
            error: null,
            reservation: null
        }
    }

    componentDidMount() {
        var that = this;
        this._easyTicketClient.getReservation(this.props.token,
            data => {
                that.setState({
                    loading: false,
                    reservation: data
                });
            },
            error => {
                this.setState({ error: error, loading: false });
            });
    }

    render() {
        if (this.state.loading) {
            return <Loading />
        }

        var errorHtml = window.easyTicket.utils.generateErrorHtml(this.state.error);
        var reservationHtml = this.getReservationHtml();

        return (
            <div>
                {reservationHtml}
                {errorHtml}
                <p><a href='/#/main'>Вернуться к оформлению бронирований</a></p>
            </div>
        );
    }

    getReservationHtml() {
        if (!this.state.reservation) {
            return "";
        }
        return (
            <div>
                <p>Ваше бронирование</p>
                <p>Токен - <i>{this.props.token}</i></p>
                <p>Идентификатор сессии - <b>{this.state.reservation.sessionId}</b></p>
                <iframe src="http://booking.uz.gov.ua/ru/" id="uz-frame"></iframe>
            </div>
        );
    }
}