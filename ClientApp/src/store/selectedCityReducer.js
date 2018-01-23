import { createSelector } from 'reselect'

const SELECT_CITY = 'SELECT_CITY'

const actionCreators = {
  selectCity(city) {
    return {
      type: SELECT_CITY,
      payload: city,
    }
  },
}

const selectors = {
  selectedCity: state => state.selectCity,
}

const reducer = (state = null, action) => {
  switch (action.type) {
    case SELECT_CITY:
      return action.payload
    default:
      return state
  }
}

export { reducer as default, actionCreators, selectors }
