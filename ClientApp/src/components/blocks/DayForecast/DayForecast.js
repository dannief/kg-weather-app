import React from 'react'
import utils from './utils'
import WeatherIcon from '../../elements/WeatherIcon'

import './DayForecast.css'

const DayForecast = ({
  location,
  dayData,
  unit,
  roundedBorderBottom = true,
}) => {
  const dayIcon = utils.getIcon(dayData.icon, dayData.description)
  const units = utils.getUnits(unit)
  return (
    <div
      className={'rw-main type-' + (roundedBorderBottom ? 'today' : '5days')}>
      <div className="rw-box-left">
        <h2 style={{ color: '#44BAF3', fontWeight: 'bold' }}>{location}</h2>
        <div className="rw-today">
          <div className="date">{utils.formatDate(dayData.date)}</div>
          <div className="hr" />
          <div className="current">
            {Math.round(dayData.temperature.avg)} {units.temp}
          </div>
          {/* <div className="range">
            {todayData.temperature.max} / {dayData.temperature.min}{' '}
            {units.temp}
          </div> */}
          <div className="hr" />
          <div className="desc">
            {/* <i className={`wicon wi ${dayIcon}`} />&nbsp; */}
            {dayData.description}
          </div>
          {/* <div className="hr" />
          <div className="info">
            <div>
              Wind Speed: <b>{dayData.wind}</b> {units.speed}
            </div>
            <div>
              Humidity: <b>{dayData.humidity}</b> %
            </div>
          </div> */}
        </div>
      </div>
      <div
        className="rw-box-right"
        style={{ color: dayData.isRainForecasted ? 'firebrick' : 'white' }}>
        <WeatherIcon name={dayIcon} />
        {/* <img src={dayData.iconUrl} style={{ width: '100%' }} alt="" /> */}
      </div>
    </div>
  )
}

export default DayForecast
