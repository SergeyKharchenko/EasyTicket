// styles
import './styles/site'
//import './css/1'

// scripts
import React from 'react';
import ReactDOM from 'react-dom';
import Application from './scripts/application';

// start
$(function() {
    ReactDOM.render(<Application />, document.getElementById('container'));
});