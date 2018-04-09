import React from 'react'
import { connect } from 'react-redux'
import { compose, lifecycle } from 'recompose'
import { withRouter } from 'react-router-dom'

import DayForecast from '../../blocks/DayForecast'
import DaysForecast from '../../blocks/DaysForecast'
import { actionCreators, selectors } from '../../../store/forecastsReducer'

const CityDaysForecast = ({
  isLoading,
  error,
  city,
  tomorrow,
  daysAfterTommorrow,
}) => {
  return isLoading ? (
    'Loading...'
  ) : error ? (
    `Error loading forecast for ${city}`
  ) : (
    <div className="rw-box">
      <DayForecast
        location={city}
        dayData={tomorrow}
        unit="metric"
        roundedBorderBottom={false}
      />
      <DaysForecast unit="metric" days={daysAfterTommorrow} />
    </div>
  )
}

const enhance = compose(
  withRouter,
  connect((state, props) => {
    return {
      city: state.selectedCity || 'Kingston',
      tomorrow: selectors.getTomorrowsForecastByCity()(state, {
        city: state.selectedCity || 'Kingston',
      }),
      daysAfterTommorrow: selectors.getDaysAfterTommorrowForecastByCity()(
        state,
        {
          city: state.selectedCity || 'Kingston',
        },
      ),
      isLoading: selectors.isFetchingForecasts(state),
      error: selectors.error(state),
    }
  }, actionCreators),
  lifecycle({
    componentDidMount() {
      this.props.loadForecasts()
    },
  }),
)

export default enhance(CityDaysForecast)
