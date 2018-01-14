import React, { PropTypes } from 'react'
import utils from './utils'
import WeatherIcon from './WeatherIcon'

const DayForecast = props => {
  const { location, todayData, unit } = props
  const todayIcon = utils.getIcon(todayData.icon)
  const units = utils.getUnits(unit)
  return (
    <div className="rw-main type-today">
      <div className="rw-box-left">
        <h2>{location}</h2>
        <div className="rw-today">
          <div className="date">{todayData.date}</div>
          <div className="hr" />
          <div className="current">
            {todayData.temperature.current} {units.temp}
          </div>
          <div className="range">
            {todayData.temperature.max} / {todayData.temperature.min}{' '}
            {units.temp}
          </div>
          <div className="desc">
            <i className={`wicon wi ${todayIcon}`} />
            &nbsp;{todayData.description}
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
      <div className="rw-box-right">
        <WeatherIcon name={todayIcon} />
      </div>
    </div>
  )
}

export default DayForecast
