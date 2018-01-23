import React from 'react'
import { Col, Grid, Row, Button } from 'react-bootstrap'
import { connect } from 'react-redux'
import { compose, lifecycle } from 'recompose'
import Equalizer from 'react-equalizer'

import DayForecast from '../../blocks/DayForecast'
import { actionCreators, selectors } from '../../../store/forecastsReducer'

const CitiesForecastSummary = ({ isLoading, forecasts, willRain }) => {
  const workersNotified = new Date().getHours() >= 16

  return isLoading ? (
    'Loading...'
  ) : (
    <Grid fluid>
      <Row>
        <Col sm={12}>
          <div style={{ marginBottom: 20 }}>
            {willRain ? (
              <div>
                <h2 style={{ color: 'firebrick' }}>
                  Rain is forecasted for tomorrow.{' '}
                  {workersNotified ? (
                    <span>Workers have been notified.</span>
                  ) : (
                    <span>
                      Workers will be notified at&nbsp;
                      <span style={{ fontWeight: 'bold' }}>4:00PM</span>.
                    </span>
                  )}
                </h2>
                {workersNotified ? null : (
                  <div style={{ textAlign: 'center' }}>
                    <Button
                      bsStyle="primary"
                      style={{ textTransform: 'uppercase' }}>
                      Notify Workers Now
                    </Button>
                  </div>
                )}
              </div>
            ) : (
              <h2>No Rain is forecasted for tomorrow</h2>
            )}
          </div>
          <hr />
        </Col>
      </Row>
      <Row>
        <Equalizer byRow={false}>
          {forecasts.map(forecast => (
            <Col
              sm={12}
              md={6}
              key={forecast.location}
              style={{ marginBottom: 40 }}>
              <DayForecast
                location={forecast.location}
                dayData={forecast.tomorrow}
                unit="metric"
              />
            </Col>
          ))}
        </Equalizer>
      </Row>
    </Grid>
  )
}

const enhance = compose(
  connect(state => {
    return {
      forecasts: selectors.tomorrowsForecast(state),
      isLoading: selectors.isFetchingForecasts(state),
      willRain: selectors.isRainForecastedTomorrow(state),
    }
  }, actionCreators),
  lifecycle({
    componentDidMount() {
      this.props.fetchCities()
    },
  }),
)

export default enhance(CitiesForecastSummary)
