import React, { PropTypes } from 'react'
import utils from './utils'
import WeatherIcon from '../WeatherIcon'

import './DayForecast.css'

const DayForecast = props => {
  const { location, todayData, unit } = props
  const todayIcon = utils.getIcon(todayData.icon)
  const units = utils.getUnits(unit)
  return (
    <div className="rw-main type-today">
      <div className="rw-box-left">
        <h2>{location}</h2>
        <div className="rw-today">
          <div className="date">{utils.formatDate(todayData.date)}</div>
          <div className="hr" />
          <div className="current">
            {Math.round(todayData.temperature.avg)} {units.temp}
          </div>
          {/* <div className="range">
            {todayData.temperature.max} / {todayData.temperature.min}{' '}
            {units.temp}
          </div> */}
          <div className="hr" />
          <div className="desc">
            {/* <i className={`wicon wi ${todayIcon}`} />&nbsp; */}
            {todayData.description}
          </div>
          {/* <div className="hr" />
          <div className="info">
            <div>
              Wind Speed: <b>{todayData.wind}</b> {units.speed}
            </div>
            <div>
              Humidity: <b>{todayData.humidity}</b> %
            </div>
          </div> */}
        </div>
      </div>
      <div
        className="rw-box-right"
        style={{ color: todayData.isRainForecasted ? 'firebrick' : 'white' }}>
        <WeatherIcon name={todayIcon} />
      </div>
    </div>
  )
}

export default DayForecast
