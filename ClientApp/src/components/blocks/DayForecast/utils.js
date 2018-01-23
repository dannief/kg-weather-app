import moment from 'moment'

export default {
  icons: {
    '1000': 'wi-day-sunny',
    '1003': 'wi-day-cloudy',
    '1006': 'wi-cloudy',
    '1009': 'wi-cloud',
    '1030': 'wi-fog',
    '1063': 'wi-day-rain',
    '1066': 'wi-day-snow',
    '1069': 'wi-day-sleet',
    '1072': 'wi-day-rain-mix',
    '1087': 'wi-day-thunderstorm',
    '1114': 'wi-day-snow-wind',
    '1117': 'wi-day-snow-thunderstorm',
    '1135': 'wi-day-fog',
    '1147': 'wi-fog',
    '1150': 'wi-day-sprinkle',
    '1155': 'wi-day-sprinkle',
    '1171': 'wi-day-rain-mix',
    '1180': 'wi-day-sprinkle',
    '1183': 'wi-day-showers',
    '1186': 'wi-day-rain',
    '1189': 'wi-day-rain',
    '1192': 'wi-day-rain',
    '1195': 'wi-day-rain',
    '1198': 'wi-day-rain-mix',
    '1201': 'wi-day-rain-mix',
    '1204': 'wi-day-sleet',
    '1207': 'wi-day-sleet',
    '1210': 'wi-day-snow',
    '1213': 'wi-day-snow',
    '1216': 'wi-day-snow',
    '1219': 'wi-day-snow',
    '1222': 'wi-day-snow',
    '1225': 'wi-day-snow-wind',
    '1237': 'wi-day-hail',
    '1240': 'wi-day-showers',
    '1243': 'wi-day-rain',
    '1246': 'wi-day-rain',
    '1252': 'wi-day-sleet',
    '1255': 'wi-day-snow',
    '1258': 'wi-day-snow',
    '1261': 'wi-day-hail',
    '1264': 'wi-day-hail',
    '1273': 'wi-day-rain',
    '1276': 'wi-day-thunderstorm',
    '1279': 'wi-day-snow',
    '1282': 'wi-day-snow-thunderstorm',
  },
  getIcon(icon, description = null) {
    if (!icon) {
      return 'wi-na'
    }
    const icoClass = this.icons[icon]
    if (icoClass) {
      return icoClass
    }
    return description && description.indexOf('rain') >= 0
      ? 'wi-day-rain'
      : 'wi-na'
  },
  getUnits(unit) {
    if (unit === 'metric') {
      return {
        temp: 'C',
        speed: 'kph',
      }
    } else if (unit === 'imperial') {
      return {
        temp: 'F',
        speed: 'mph',
      }
    }
    return { temp: '', speed: '' }
  },
  formatDate(dte) {
    if (dte && moment(dte).isValid()) {
      return moment(dte).format('ddd D MMM')
    }
    return ''
  },
}
