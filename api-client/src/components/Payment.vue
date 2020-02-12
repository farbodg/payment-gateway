<template>
  <div class="payment">
    <h1>Welcome to Payment Gateway</h1>
    <div class="container">
      <div class="retrieve-payment">
        <h3>Retrieve details about a payment:</h3>
        <input v-model="paymentId" placeholder="Payment Id">
        <br>
        <button v-on:click="retrievePaymentDetails">Retrieve Details</button>
        <br>
        <span class="error" v-if="paymentDetailError">{{paymentDetailError}}</span>
        <span v-if="!paymentDetailError && paymentDetailsDto.amount">Payment of {{paymentDetailsDto.amount}} {{paymentDetailsDto.currency}} made by {{paymentDetailsDto.card.cardHolderName}} {{paymentDetailsDto.card.cardNumber}} on {{paymentDetailsDto.paymentDate}}</span>
      </div>
      <div class="">
        <h3>Process a payment:</h3>
        <input v-model.number="paymentRequestDto.merchantId" placeholder="Merchant Id">
        <input v-model.number="paymentRequestDto.amount" placeholder="Amount">
        <input v-model="paymentRequestDto.currency" placeholder="Currency">
        <input v-model="paymentRequestDto.card.cardNumber" placeholder="Card Number">
        <input v-model="paymentRequestDto.card.cardHolderName" placeholder="Card Holder Name">
        <input v-model="paymentRequestDto.card.expiryDate" placeholder="Expiry Date">
        <input v-model.number="paymentRequestDto.card.cvv" placeholder="CVV">
        <br>
        <button v-on:click="postPayment">Process payment</button>
        <br>
        <span class="error" v-if="paymentError">{{paymentError}}</span>
        <span v-if="!paymentError && paymentResponseDto && paymentResponseDto.id">Payment Success! Payment Id #{{paymentResponseDto.id}}</span>
      </div>
    </div>
  </div>
</template>

<script>
import api from './api';
export default {
  name: 'Payment',
  props: {
    msg: String
  },
  data () {
    return {
      paymentId: null,
      paymentError: null,
      paymentDetailError: null,
      paymentRequestDto: {
        merchantId: null,
        currency: null,
        amount: null,
        card: {
          cardNumber: null,
          cardHolderName: null,
          expiryDate: null,
          cvv: null
        }
      },
      paymentResponseDto: {
        id: null,
        status: null
      },
      paymentDetailsDto: {
        amount: null,
        currency: null,
        card: {
          cardNumber: null,
          cardHolderName: null
        },
        paymentDate: null
      }
    }
  },
  methods : {
    retrievePaymentDetails : async function () {
      this.paymentDetailError = null;

      try {
        const result = await api.getPaymentDetails(this.paymentId);
        this.paymentDetailsDto = result.data;
        this.paymentId = null;
      }
      catch (exception) {
        this.paymentDetailError = "Payment not found.";
      }
    },
    postPayment : async function () {
      try {
        this.paymentResponseDto = null;
        this.paymentError = null;

        const result = await api.postPayment((this.paymentRequestDto));
        this.paymentResponseDto = result.data;
      }
      catch (error) {
        const errors = error.response.data.errors;
        for (let [key] of Object.entries(errors)) {
          this.paymentError = errors[key][0];
          break;
        }
      }
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
input {
 padding: 5px;
 font-size: 14px;
}
.container {
  display: flex;
}
.container > div {
  flex: 1;
}
.payment {
  margin: auto;
  width: 600px;
}
.error {
  color: red;
  font-size: 14px;
}
</style>
