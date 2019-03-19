const fs = require('fs');
let text = fs.readFileSync('./test.txt');
const regExpSimbols = /[ |0-9|,|;|:|\/|'|.|~|"|(|)|=|-|—|^|?|*|&|%|$|#|!|@|+|\||<|>|\\|\r|\n|\t]/g;
const alfabhetENG = 'abcdefghigklmnopqrstuvwxyz';

let hartley = function(n) {
  return Math.log2(n);
}

let shanon = function(str, alfabhet) {
  let I = 0;
  for(let i = 0; i < alfabhet.length; i++) {    
    let reg = new RegExp(alfabhet.charAt(i), 'g'),
        symbol = alfabhet.charAt(i), 
        p = (str.match(reg) === null) ? 0 : str.match(reg).length / str.length;
    
    console.log(`символ: '${symbol}', p(${symbol}) = ${p}`);
    if(p !== 0) I += p * Math.log2(p);
  }

  return -I;
}

let shanonInfoAmount = function (str, alfabhet) {
    return str.length * shanon(str, alfabhet);
}

let hartleyInfoAmount = function (str, alfabhet) {
    return str.length * hartley(alfabhet.length);
}

text = text.toString().toLowerCase().replace(regExpSimbols, '');
const str = 'Dubovik Marina Vladimirovna'.toLowerCase().replace(regExpSimbols, '');

console.log(`Длина текста = ${text.length}`);
console.log(`Энтропия по Хартли: ${hartley(alfabhetENG.length)}`);
console.log(`Энтропия по Шеннону: ${shanon(text, alfabhetENG)}`);

console.log('----------------------------');

console.log(str);
console.log(`Количество информации(по Шеннону): ${shanonInfoAmount(str, alfabhetENG)}`);
console.log(`Количество информации(по Хартли): ${hartleyInfoAmount(str, alfabhetENG)}`);
