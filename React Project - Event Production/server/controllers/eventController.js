import express from 'express'
const router = express.Router();
import Evenent from '../Modules/productionTypeModule.js'

router.get('/api/getEvents', async (req, res) => {
    try{
        const p = await Evenent.find();
        if (!p) {
            return res.status(500).send({ message: 'No events found' });
        }
        res.send(p);
    }
    catch(err){
        res.status(500).send(err);
    }
});

router.get('/api/getEvent/:_id', async (req, res) => {
    console.log(typeof(req.params._id)+"typeof")
    // if(typeof(req.params._id) != "string")
    //     return res.status(400).send('הנתונים שגויים!!')
    try{
        // const p = await Evenent.findOne({ _id: req.params._id});
        console.log(req.params._id+"in try")
        const p = await Evenent.findOne({ _id: req.params._id });
        console.log("חדש---------------")
        if (!p) {
            return res.status(500).send({ message: 'This event doesn\'t exist' });
        }
        console.log("producerId: " + p._id + " ,eventName: " + p.name);
        res.send(p);
    }
    catch(err){
        res.status(500).send(err);
    }
});

router.post('/postEvent', async (req, res)=>{
    console.log(typeof(req.params.name))
    if( typeof(req.body.name) != "string" || typeof(req.body.descreption) != "string" ||typeof(req.body.ProducerMail) != "string"){
        return res.status(400).send("הנתונים שגויים!!")
    }
    try{
        console.log("in postEventProducer")
        const event = await new Evenent({
            name: req.body.name,
            descreption: req.body.descreption,
            ProducerMail: req.body.ProducerMail
        })
        event.save()
        console.log(event)
        res.send({ message: 'Data received successfully!' });
    }
    catch(err){
        res.status(500).send(err);
    }
})

router.put('/putEvent', async (req, res)=>{
    if(typeof(req.body.name) != "string" || typeof(req.body.descreption) != "string" ||typeof(req.body.ProducerMail) != "string"){
        return res.status(400).send("הנתונים שגויים!!")
    }
    try{
        const p = await Evenent.findOne({ id: req.body._id });
        if (!p) {
            return res.status(500).send({ message: 'This producer doesn\'t exist' });
        }
        await Evenent.updateOne({_id:req.body._id}, req.body);
        res.send(p.name +" is update")
    }
    catch(error) {
        res.status(500).send(error);
    }
});

router.delete('/deleteEvent/:_id', async (req, res)=>{
    console.log("in controller in deleteEvent----------------------------------------")
    if(typeof(req.params._id) != "string")
        return res.status(400).send('הנתונים שגויים!!')
    try{
        const p = await Evenent.findOne({ _id: req.params._id });
        if (!p) {
            return res.status(500).send({ message: 'This producer doesn\'t exist' });
        }
        await Evenent.deleteOne({_id:req.params._id});
        res.send(p.name +" is deleted")
    }
    catch(error) {
        res.status(500).send(error);
    }
});

export default router;