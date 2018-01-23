import React, { PropTypes } from 'react'
import utils from '../DayForecast/utils'
import WeatherIcon from '../../elements/WeatherIcon'

const DaysForecast = ({ unit, days }) => {
  const units = utils.getUnits(unit)
  return (
    <div className="rw-box-days">
      {days.map(day => {
        const iconCls = utils.getIcon(day.icon)
        return (
          <div key={day.date} className="rw-day">
            <div className="rw-date">{utils.formatDate(day.date)}</div>
            <WeatherIcon name={iconCls} />
            <div className="rw-desc">{day.description}</div>
            <div className="rw-range">
              {day.temperature.max} / {day.temperature.min} {units.temp}
            </div>
          </div>
        )
      })}
    </div>
  )
}

export default DaysForecast
