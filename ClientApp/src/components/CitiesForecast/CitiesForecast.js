import React from 'react'
import { Col, Grid, Row } from 'react-bootstrap'
import { connect } from 'react-redux'
import { compose, lifecycle } from 'recompose'

import DayForecast from '../DayForecast'
import { actionCreators, selectors } from '../../store/forecastsReducer'

const CitiesForecastSummary = ({ isLoading, forecasts }) =>
  isLoading ? (
    'Loading...'
  ) : (
    <Grid fluid>
      <Row>
        {forecasts.map(forecast => (
          <Col sm={12} md={6} key={forecast.location}>
            <DayForecast
              location={forecast.location}
              todayData={forecast.days[1]}
              unit="metric"
            />
            <br />
            <br />
          </Col>
        ))}
      </Row>
    </Grid>
  )

const enhance = compose(
  connect(state => {
    return {
      forecasts: selectors.forecastsArray(state),
      isLoading: selectors.isFetchingForecasts(state),
    }
  }, actionCreators),
  lifecycle({
    componentDidMount() {
      this.props.fetchCities()
    },
  }),
)

export default enhance(CitiesForecastSummary)
