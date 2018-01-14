import React from 'react'
import { Route } from 'react-router'
import Layout from './components/Layout'
import Home from './components/Home'
import Counter from './components/Counter'
import FetchData from './components/FetchData'
import CitiesForecastSummary from './components/CitiesForecastSummary'

import 'react-open-weather/lib/css/ReactWeather.css'

export default () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <Route path="/counter" component={Counter} />
    <Route path="/fetchdata/:startDateIndex?" component={FetchData} />
    <Route path="/forecasts" component={CitiesForecastSummary} />
  </Layout>
)
