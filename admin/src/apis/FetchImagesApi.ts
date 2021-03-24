import axios from 'axios';
const imagesFetch = async () => {
    const response = await axios.get('https://localhost:5001/api/v1/Files');
    return response.data;
}
export default imagesFetch;