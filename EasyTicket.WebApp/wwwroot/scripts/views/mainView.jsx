import React from 'react';
import StationSelect from './../controls/stationSelect';
import DateInput from './../controls/dateInput';
import TextInput from './../controls/textInput';
import EmailInput from './../controls/emailInput';
import GroupSelectInput from './../controls/groupSelectInput';
import SelectInput from './../controls/selectInput';

export default class MainView extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            firstName: null,
            lastName: null,
            email: null,
            date: null,
            wagonType: "Economy",
            placesCount: this.getPlacesCount("Economy"),
            selectedPlaces: []
        };
    }

    componentDidMount() {
       $(`#places`).material_select();
    }

    render() {
        var places = [];
        for (var i = 0; i < this.state.placesCount; i++) {
            places.push({value: i, text: i});
        }

        return (
            <div>
                <h4>Автоматизированная покупка билетов на Укрзализныци</h4>
                <div className='row'>
                    <div className='col s12 m6'>
                        <StationSelect label='Станция отправления' onStationSelected={this.onStationFromSelected.bind(this)}/>
                    </div>
                    <div className='col s12 m6'>
                        <StationSelect label='Станция прибытия' onStationSelected={this.onStationToSelected.bind(this)}/>
                    </div>
                </div>
                
                <div className='row'>
                    <div className="input-field col s12">
                        <DateInput id="date" label="Дата поездки"/>
                    </div>
                </div>

                <div className='row'>
                    <div className="input-field col s12 m6">
                        <TextInput id="first-name" label="Имя" icon="account_circle"/>
                    </div>
                    <div className="input-field col s12 m6">
                        <TextInput id="last-name" label="Фамилия" icon="supervisor_account"/>
                    </div>
                </div>

                <div className='row'>
                    <div className="input-field col s12">
                        <EmailInput id="email" label="Почта"/>
                    </div>
                </div>

                <div className='row'>
                    <div className="input-field col s12">
                        <GroupSelectInput id="wagon-type" label="Тип вагона" icon="settings"
                            onChange={this.onWagonTypeChange.bind(this)}
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
                            multiple options={places} />
                    </div>
                    <div className="input-field col s12 m6">
                        <SelectInput id="search-type" label="Тип поиска" icon="search" options={[{
                            value: "All", text: "Все"
                        }, {
                            value: "Any", text: "Любой из"
                        }]} />
                    </div>
                </div>

                <p><a href='/#/request'>Go to Request page</a></p>
            </div>
        );
    }

    onStationFromSelected(station) {
    }

    onStationToSelected(station) {
    }

    onWagonTypeChange(value) {
        this.setState({ selectedPlaces: [], placesCount: this.getPlacesCount(value) });
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
}