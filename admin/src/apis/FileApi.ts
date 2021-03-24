//import axios from 'src/utils/axios';
import axios from 'axios';

const imageUpload = async (files) => {
    const formData = new FormData();
    formData.append('file', files[0]);
    console.log('formdata', files[0]);
    //const response = await axios.post<{ accessToken: string; }>('/api/account/login', { files });
    const response = await axios.post('https://localhost:5001/api/v1/Files', formData);
    console.log(response.data);
    return response;
}

export default imageUpload;

