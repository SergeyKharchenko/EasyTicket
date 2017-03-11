import React from 'react';
import StationSelect from './stationSelect'

export default class MainView extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <p>This is Main</p>
                <div className='row'>
                    <div className='col s6'>
                        <StationSelect label='Станция отправления' onStationSelected={this.onStationFromSelected.bind(this)}/>
                    </div>
                    <div className='col s6'>
                        <StationSelect label='Станция прибытия' onStationSelected={this.onStationToSelected.bind(this)}/>
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
}