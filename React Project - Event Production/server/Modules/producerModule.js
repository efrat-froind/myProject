import mongoose from 'mongoose';
const Schema = mongoose.Schema;

mongoose.connect("mongodb://127.0.0.1:27017/EventProduction");

const producer= new Schema({
    name: String,
    email: String,
    phone: String,
    shortDescreption: String
});

const Producer = mongoose.model('Producer', producer)
export default Producer;