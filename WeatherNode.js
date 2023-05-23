const { MongoClient } = require('mongodb')
const axios = require('axios')
const client = new MongoClient("mongodb+srv://elias:rrelias09@cluster0.jfq6e7e.mongodb.net/")
const db = client.db('WeatherDB')

setInterval(() => {
  axios.get("https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?period=latest-10-minutes&api-key=1fa153cf-5edb-40f7-996b-14adcdc497f4")
        .then((response) => {
          db.collection("Weather").insertOne(response.data)
          console.log(`loaded successfully: ${new Date().toLocaleString('en-GB')}`)
        })
        .catch((error) => console.error(`${error.message}\n${new Date().toLocaleString('en-GB')}`))
}, 600000)