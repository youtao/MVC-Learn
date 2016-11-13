let express = require('express');
let nunjucks = require('nunjucks');
let app = express();
app.use('/static',express.static('static'));
app.use('/~/static',express.static('static'));
nunjucks.configure('views',{
    express: app,
    watch: true,
    tags: {
        blockStart: '{{%',
        blockEnd: '%}}',
        variableStart: '{{$',
        variableEnd:'$}}'
    }
});
app.listen(4000, () => {

});

app.get('/', (req, res) => {
    res.render('home.html', {
        title: 'home',
        name:'youtao'
    });
});