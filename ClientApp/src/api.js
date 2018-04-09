export async function fetchWeatherForecast() {
  return await fetchData('v1/weather/forecast')
}

export async function fetchCities() {
  return await fetchData('v1/reference-values/cities')
}

export async function notifyWorkers() {
  const url = 'v1/schedules/notifications'
  const response = await fetch(url, { method: 'POST' })
  if (!response.ok) {
    throw new Error(response.statusText)
  }
}

async function fetchData(url) {
  const response = await fetch(url)
  if (!response.ok) {
    throw new Error(response.statusText)
  }
  const data = (await response.json()).data
  return data
}
