import { createSelector } from 'reselect'

const FETCH_FORECASTS = 'FETCH_FORECASTS'

const actionCreators = {
  fetchCities() {
    return async (dispatch, getState) => {
      // If we have already loaded the forecast, don't load them again
      if (getState().forecast) {
        return
      }

      const url = `v1/weather/forecast`
      const response = await fetch(url)
      const data = (await response.json()).data

      const forecasts = data.reduce((forecastMap, forecast) => {
        forecastMap[forecast.city] = {
          location: forecast.city,
          days: forecast.days.map(day => {
            return {
              ...day,
              description: day.summary,
              icon: day.iconCode,
            }
          }),
        }
        return forecastMap
      }, {})

      dispatch({ type: FETCH_FORECASTS, payload: forecasts })
    }
  },
}

const selectors = (function() {
  const forecasts = state => state.forecasts

  const isFetchingForecasts = createSelector(
    [forecasts],
    forecasts => !forecasts,
  )

  const forecastsArray = createSelector(
    [forecasts],
    forecasts => forecasts && Object.values(forecasts),
  )

  const tomorrowsForecast = createSelector(
    [forecastsArray],
    forecastsArray =>
      forecastsArray &&
      forecastsArray.map(f => {
        return { location: f.location, tomorrow: f.days[1] }
      }),
  )

  const isRainForecastedTomorrow = createSelector(
    [tomorrowsForecast],
    tommForecasts =>
      tommForecasts && tommForecasts.some(f => f.tomorrow.isRainForecasted),
  )

  const forecastDaysByCity = (state, props) =>
    state.forecasts && state.forecasts[props.city].days

  const getTomorrowsForecastByCity = () =>
    createSelector([forecastDaysByCity], days => days && days[1])

  const getDaysAfterTommorrowForecastByCity = () =>
    createSelector([forecastDaysByCity], days => days && days.slice(2))

  return {
    forecasts,
    isFetchingForecasts,
    forecastsArray,
    tomorrowsForecast,
    isRainForecastedTomorrow,
    forecastDaysByCity,
    getTomorrowsForecastByCity,
    getDaysAfterTommorrowForecastByCity,
  }
})()

const reducer = (state = null, action) => {
  switch (action.type) {
    case FETCH_FORECASTS:
      return action.payload
    default:
      return state
  }
}

export { reducer as default, actionCreators, selectors }
