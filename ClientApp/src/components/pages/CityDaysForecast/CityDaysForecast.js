import React from 'react'
import { connect } from 'react-redux'
import { compose, lifecycle } from 'recompose'

import DayForecast from '../../blocks/DayForecast'
import DaysForecast from '../../blocks/DaysForecast'
import { actionCreators, selectors } from '../../../store/forecastsReducer'

const CityDaysForecast = ({
  isLoading,
  city,
  tomorrow,
  daysAfterTommorrow,
}) => {
  return isLoading ? (
    'Loading...'
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
  connect((state, props) => {
    return {
      city: 'Kingston',
      tomorrow: selectors.getTomorrowsForecastByCity()(state, {
        city: 'Kingston',
      }),
      daysAfterTommorrow: selectors.getDaysAfterTommorrowForecastByCity()(
        state,
        {
          city: 'Kingston',
        },
      ),
      isLoading: selectors.isFetchingForecasts(state),
    }
  }, actionCreators),
  lifecycle({
    componentDidMount() {
      this.props.fetchCities()
    },
  }),
)

export default enhance(CityDaysForecast)
