import React from 'react'
import { Route, Redirect } from 'react-router'
import Layout from './components/layouts/DefaultLayout'
import CitiesForecast from './components/pages/CitiesForecast'
import CityDaysForecast from './components/pages/CityDaysForecast'

import 'react-open-weather/lib/css/ReactWeather.css'

export default () => (
  <Layout>
    <Route exact path="/" render={props => <Redirect to="/forecasts" />} />
    <Route path="/forecasts" component={CitiesForecast} />
    <Route path="/cities" component={CityDaysForecast} />
  </Layout>
)
