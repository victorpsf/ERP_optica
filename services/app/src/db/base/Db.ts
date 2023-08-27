import axios from 'axios'

export default axios.create({
    baseURL: 'http://localhost:5297',
    timeout: 12000
})