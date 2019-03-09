const fs = require('fs');

let text = fs.readFileSync('file.txt', 'utf8');
text = text.toLowerCase();

let letters = {};
let len = 0;
let p;
let shennon = 0;
let symbols = 0;
let hartley;

for(let i = 0; i < text.length; i++){
    if(text[i] >= 'a' && text[i] <= 'z'){
        len++;
        if(letters[text[i]] == undefined){
            letters[text[i]] = 0;
            symbols++;
        }
        else{
            letters[text[i]]++;
        }
    }
}

console.log('text length = ' + len);

for(let letter in letters){
    p = letters[letter] / len;
    console.log(letter + ' = ' + letters[letter] + '  \t|\t' + p);
    shennon += p * Math.log2(p);
}
shennon = shennon * (-1);
console.log('shennon entrophy = ' + shennon);

hartley = Math.log2(symbols);
console.log('hartley enthropy = ' + hartley);

//**********************************************************
//**********************************************************

const FIO = 'pakholkoalyonastepanovnaa';

let amountOfInformation = shennon * FIO.length;

console.log('Amount of information in my name = ' + amountOfInformation);
