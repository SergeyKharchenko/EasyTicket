import React from 'react';
import StationSelect from './../controls/stationSelect';
import DateInput from './../controls/dateInput';
import TextInput from './../controls/textInput';
import EmailInput from './../controls/emailInput';
import GroupSelectInput from './../controls/groupSelectInput';
import SelectInput from './../controls/selectInput';
import Button from './../controls/button';

import Toast from './../infrastructure/toast';

import EasyTicketClient from './../infrastructure/easyTicketClient'

export default class MainView extends React.Component {
    constructor(props) {
        super(props);

        this._easyTicketClient = new EasyTicketClient();

        this.state = {
            request: {
                firstName: null,
                lastName: null,
                email: null,
                date: null,
                stationFrom: null,
                stationTo: null,
                wagonType: "Economy",
                places: [],
                searchType: 'Any'
            },
            placesCount: this.getPlacesCount("Economy"),
            requesting: false,
            error: null,
        };
    }

    componentDidMount() {
        $(`#places`).material_select();
    }

    render() {
        var places = [];
        for (var i = 0; i < this.state.placesCount; i++) {
            places.push({ value: i, text: i });
        }

        var error = window.easyTicket.utils.generateErrorHtml(this.state.error);

        return (
            <div>
                <h4>Автоматизированная покупка билетов на Укрзализныци</h4>
                <div className='row'>
                    <div className='col s12 m6'>
                        <StationSelect label='Станция отправления' onStationSelected={this.onStationFromSelected.bind(this)} />
                    </div>
                    <div className='col s12 m6'>
                        <StationSelect label='Станция прибытия' onStationSelected={this.onStationToSelected.bind(this)} />
                    </div>
                </div>

                <div className='row'>
                    <div className="input-field col s12">
                        <DateInput id="date" label="Дата поездки" onChange={this.onDateChanged.bind(this)} />
                    </div>
                </div>

                <div className='row'>
                    <div className="input-field col s12 m6">
                        <TextInput id="first-name" label="Имя" icon="account_circle" onChange={this.onFirstNameChanged.bind(this)} />
                    </div>
                    <div className="input-field col s12 m6">
                        <TextInput id="last-name" label="Фамилия" icon="supervisor_account" onChange={this.onLastNameChanged.bind(this)} />
                    </div>
                </div>

                <div className='row'>
                    <div className="input-field col s12">
                        <EmailInput id="email" label="Почта" onChange={this.onEmailChanged.bind(this)} />
                    </div>
                </div>

                <div className='row'>
                    <div className="input-field col s12">
                        <GroupSelectInput id="wagon-type" label="Тип вагона" icon="settings"
                            onChange={this.onWagonTypeChanged.bind(this)}
                            groups={[
                                {
                                    header: "Обычный вагон",
                                    options: [{
                                        text: "Плацкарт", value: "Economy",
                                    }, {
                                        text: "Купэ", value: "Coupe",
                                    }]
                                }
                            ]} />
                    </div>
                </div>
                <div className="row">
                    <div className="input-field col s12 m6">
                        <SelectInput id="places" label="Место" placeholder="Выберите одно или несколько мест" icon="perm_contact_calendar"
                            multiple options={places} onChange={this.onPlacesChanged.bind(this)} />
                    </div>
                    <div className="input-field col s12 m6">
                        <SelectInput id="search-type" label="Тип поиска" icon="search" options={[{
                            value: "Any", text: "Любой из"
                        }, {
                            value: "All", text: "Все"
                        }]} onChange={this.onSearchTypeChanged.bind(this)} />
                    </div>
                </div>
                <div className="row">
                    <div className="col m6 s12">
                        <Button text="Добавить запрос" icon="send" onClick={this.onRequestButtonClick.bind(this)}
                            { ...(!this.canExecuteRequest() && { disabled: true }) } />
                    </div>
                </div>

                <p><a href='/#/request'>Go to Request page</a></p>
                {error}
            </div>
        );
    }

    onStationFromSelected(station) {
        var request = this.state.request;
        request.stationFrom = station;
        this.setState({request: request});
    }

    onStationToSelected(station) {
        var request = this.state.request;
        request.stationTo = station;
        this.setState({request: request});
    }

    onDateChanged(date) {
        var request = this.state.request;
        request.date = date;
        this.setState({request: request});
    }

    onFirstNameChanged(firstName) {
        var request = this.state.request;
        request.firstName = firstName;
        this.setState({ request: request });
    }

    onLastNameChanged(lastName) {
        var request = this.state.request;
        request.lastName = lastName;
        this.setState({ request: request });
    }

    onEmailChanged(email) {
        var request = this.state.request;
        request.email = email;
        this.setState({ request: request });
    }

    onWagonTypeChanged(wagonType) {
        var request = this.state.request;
        request.wagonType = wagonType;
        request.places = [];
        this.setState({
            placesCount: this.getPlacesCount(wagonType),
            request: request
        });
    }

    getPlacesCount(wagonType) {
        var placesCount;
        switch (wagonType) {
            case "Economy": {
                return 54;
            }
            case "Coupe": {
                return 40;
            }
        }
    }

    onPlacesChanged(places) {
        var request = this.state.request;
        request.places = places;
        this.setState({ request: request });
    }

    onSearchTypeChanged(searchType) {
        var request = this.state.request;
        request.searchType = searchType;
        this.setState({ request: request });
    }

    onRequestButtonClick() {
        if (!this.canExecuteRequest) {
            return;
        }
        this.setState({ requesting: true });

        var req = this.state.request;
        var request = {
            stationFromId: req.stationFrom.id,
            stationFromTitle: req.stationFrom.title,
            stationToId: req.stationTo.id,
            stationToTitle: req.stationTo.title,
            date: this.toUZFormatDate(req.date),
            passangerName: req.firstName,
            passangerSurname: req.lastName,
            passangerEmail: req.email,
            wagonType: req.wagonType,
            places: req.places,
            searchType: req.searchType
        };

        var that = this;
        this._easyTicketClient.sendRequest(request,
            data => {
                that.setState({
                    requesting: false
                });
                var toast = new Toast("Ваш запрос успешно создан");
                toast.show();
            }, 
            error => {
                this.setState({ error: error, requesting: false });
            });
    }

    toUZFormatDate(date) {
        var mm = date.getMonth() + 1; // getMonth() is zero-based
        var dd = date.getDate();

        return [(dd > 9 ? '' : '0') + dd,
            (mm > 9 ? '' : '0') + mm,
            date.getFullYear()
            ].join('.');
    };

    canExecuteRequest() {
        var req = this.state.request;
        return !this.state.requesting
            && !!req.date && !!req.firstName && !!req.lastName && !!req.email 
            && !!req.stationFrom && !!req.stationTo
            && !!req.places.length;
    }
}