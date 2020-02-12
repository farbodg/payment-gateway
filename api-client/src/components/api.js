import axios from 'axios';

const config = {
    headers: {
        'Access-Control-Allow-Origin': 'https://localhost'
    }
};

const rootApiUrl = 'https://localhost:5001/api/payments/';

export default {
    async getPaymentDetails(id) {
        return axios.get(encodeURI(rootApiUrl + id), config);
    },
    async postPayment(paymentRequestDto) {
        return axios.post(encodeURI(rootApiUrl), paymentRequestDto, config);
    }
}

