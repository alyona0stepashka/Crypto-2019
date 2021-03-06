//cesar

function cesarCrypt(text, key){
    text = text.toUpperCase();
    let rc = '';
    for(let i = 0; i < text.length; i++){
        let ind = (text.charCodeAt(i) - 65 + key) % 26 + 65;
        rc += String.fromCharCode(ind);
    }
    return rc;
}
//vizhener

function vizhenerCrypt(text, key){
    text = text.toUpperCase();
    key = key.toUpperCase();
    let rc = '';

    while(text.length > key.length){
        key += key;
    }
    key = key.slice(0, text.length);

    for(let i = 0; i < text.length; i++){
        let ind = (text.charCodeAt(i) + key.charCodeAt(i) - 130) % 26 + 65;
        rc += String.fromCharCode(ind);
    }
    console.log(text);
    console.log(key);
    console.log(rc);
    return rc;
}

function MyNewCipher(text){
  let rc ='';
  let key =0;
  text=text.toUpperCase();
  for (let i=text.length-1;i>=0;i--)
  {
    rc += text[i];
}
return rc;
}


function MyCipher(text){
  let rc ='';
  text=text.toUpperCase();
  for (let i=0;i<text.length;i++)
  {
    let ind = (text.length-i) * (text.charCodeAt(i) - 65 + 1) % 26 + 65;
    rc += String.fromCharCode(ind);
}
return rc;
}



//E(x) = (ax +b) mod m
//D = a(-1)*(x-b) mod m

//affinny
function affynnyCrypt(text){
    let a = 3;
    let b = 4;
    let m = 26;
    text = text.toUpperCase();
    let rc = '';
    for(let i = 0; i < text.length; i++){
        let ind = (((a * (text.charCodeAt(i) - 65)) + b) % m) + 65;
        rc += String.fromCharCode(ind);
    }
    return rc;
}


console.log(cesarCrypt('abcdefgh', 1));
console.log('---/Cezar/------------------------');

vizhenerCrypt('ATTACKATDAWN', 'LEMON');
console.log('---/Vizhener/---------------------');

console.log(affynnyCrypt('ATTACKATDAWN'));                                                                                                                                                                            

console.log(MyCipher('ATTACKATDAWN'));
console.log('---/MyCipher/---------------------');

console.log(MyNewCipher('ATTACKATDAWN'));
console.log('---/New/--------------------------');
