import React from 'react'
import { Col, Grid, Row, Button } from 'react-bootstrap'
import { connect } from 'react-redux'
import {
  compose,
  lifecycle,
  withHandlers,
  withState,
  withStateHandlers,
} from 'recompose'
import Equalizer from 'react-equalizer'
import { withRouter } from 'react-router-dom'

import DayForecast from '../../blocks/DayForecast'
import {
  actionCreators as frActionCreators,
  selectors as frSelectors,
} from '../../../store/forecastsReducer'
import {
  actionCreators as scActionCreators,
  selectors as scSelectors,
} from '../../../store/selectedCityReducer'
import { notifyWorkers } from '../../../api'

const CitiesForecastSummary = ({
  isLoading,
  error,
  forecasts,
  willRain,
  selectCity,
  history,
  notify,
  notifySuccess,
}) => {
  const workersNotified = new Date().getHours() >= 16

  return isLoading ? (
    'Loading...'
  ) : error ? (
    'Error loading forecast'
  ) : (
    <Grid fluid>
      <Row>
        <Col sm={12}>
          <div style={{ marginBottom: 20 }}>
            {willRain ? (
              <div>
                <h2 style={{ color: 'darkgreen' }}>
                  Rain is forecasted for tomorrow.{' '}
                  {workersNotified ? (
                    <span>Workers have been notified.</span>
                  ) : (
                    <span>
                      Workers will be notified automatically at&nbsp;
                      <span style={{ fontWeight: 'bold' }}>4:00PM</span>.
                    </span>
                  )}
                </h2>
                {workersNotified ? null : (
                  <div style={{ textAlign: 'center' }}>
                    <Button
                      bsStyle="primary"
                      style={{ textTransform: 'uppercase' }}
                      onClick={notify}>
                      Notify Workers Now
                    </Button>
                    {notifySuccess === false ? (
                      <h3 style={{ color: 'firebrick' }}>
                        Notification failed!
                      </h3>
                    ) : notifySuccess === true ? (
                      <h3>
                        Workers notified at {new Date().toLocaleTimeString()}
                      </h3>
                    ) : null}
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
              style={{ marginBottom: 40 }}
              onClick={() => {
                selectCity(forecast.location)
                history.push(`/cities?city=${encodeURI(forecast.location)}`)
              }}>
              <DayForecast
                location={forecast.location}
                m
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
      forecasts: frSelectors.tomorrowsForecast(state),
      isLoading: frSelectors.isFetchingForecasts(state),
      willRain: frSelectors.isRainForecastedTomorrow(state),
      error: frSelectors.error(state),
    }
  }, Object.assign({}, frActionCreators, scActionCreators)),
  lifecycle({
    componentDidMount() {
      this.props.loadForecasts()
    },
  }),
  withState('notifySuccess', 'setNotifySuccess', null),
  withHandlers({
    notify: ({ setNotifySuccess }) => async () => {
      try {
        setNotifySuccess(null)
        await notifyWorkers()
        setNotifySuccess(true)
      } catch (error) {
        setNotifySuccess(false)
      }
    },
  }),
  withRouter,
)

export default enhance(CitiesForecastSummary)
