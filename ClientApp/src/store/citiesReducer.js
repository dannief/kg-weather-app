import { createSelector } from 'reselect'

const FETCH_CITIES = 'FETCH_CITIES'

const actionCreators = {
  requestCities() {
    return async (dispatch, getState) => {
      // If we have already loaded the cities, don't load them again
      if (getState().cities) {
        return
      }

      const url = `v1/reference-values/cities`
      const response = await fetch(url)
      const cities = (await response.json()).data

      dispatch({ type: FETCH_CITIES, payload: cities })
    }
  },
}

const selectors = (function() {
  const cities = state => state.cities
  const isFetchingCities = createSelector([cities], cities => !cities)

  return {
    cities,
    isFetchingCities,
  }
})()

const reducer = (state = null, action) => {
  switch (action.type) {
    case FETCH_CITIES:
      return action.payload
    default:
      return state
  }
}

export { reducer as default, actionCreators }
