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

const selectors = {
  forecasts: state => state.forecasts,
  get isFetchingForecasts() {
    return createSelector([this.forecasts], forecasts => !forecasts)
  },
  get forecastsArray() {
    return createSelector(
      [this.forecasts],
      forecasts => forecasts && Object.values(forecasts),
    )
  },
}

const reducer = (state = null, action) => {
  switch (action.type) {
    case FETCH_FORECASTS:
      return action.payload
    default:
      return state
  }
}

export { reducer as default, actionCreators, selectors }
