import { createSelector } from 'reselect'
import { fetchCities } from '../api'

const FETCH_CITIES = 'FETCH_CITIES'

const actionCreators = {
  loadCities() {
    return async (dispatch, getState) => {
      // If we have already loaded the cities, don't load them again
      if (getState().cities) {
        return
      }

      try {
        const cities = await fetchCities()
        dispatch({ type: FETCH_CITIES, payload: cities })
      } catch (error) {
        dispatch({ type: FETCH_CITIES, payload: error, error: true })
      }
    }
  },
}

const selectors = (function() {
  const cities = state => state.cities && state.cities.data
  const isFetchingCities = state => !state.cities
  const error = state => state.cities && state.cities.error

  return {
    cities,
    isFetchingCities,
  }
})()

const reducer = (state = null, action) => {
  switch (action.type) {
    case FETCH_CITIES:
      return action.error
        ? { data: null, error: true }
        : { data: action.payload, error: false }
    default:
      return state
  }
}

export { reducer as default, actionCreators }
