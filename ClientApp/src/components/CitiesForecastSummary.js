import React, { Component } from 'react'
import { connect } from 'react-redux'
import TodayForecast from 'react-open-weather/src/js/components/TodayForecast'

const CitiesForecastSummary = () => (
  <TodayForecast
    todayData={{
      icon: 1009,
      temperature: {
        current: 78,
        min: 76,
        max: 80,
      },
      description: 'Partly cloudy',
      wind: 100,
      humidity: 10,
    }}
    unit="imperial"
  />
)

export default CitiesForecastSummary
