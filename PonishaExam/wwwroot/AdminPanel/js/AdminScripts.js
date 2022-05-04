// Sum Numbers
const testCounts = document.querySelectorAll('.testCounts');
const correctAnswers = document.querySelectorAll('.correctAnswers');
const wrongAnswers = document.querySelectorAll('.wrongAnswers');
const noCheckedAnswers = document.querySelectorAll('.noCheckedAnswers');
const showCount = document.getElementById('testsCount');
const showCorrect = document.getElementById('correctSum');
const showWrong = document.getElementById('wrongSum');
const showNoChecked = document.getElementById('noCheckedSum');
var sumCount = 0;
var sumCorrect = 0;
var sumWrong = 0;
var sumNoChecked = 0;
for (i = 0; i < testCounts.length; i++) {
    sumCount = +sumCount + +testCounts[i].value;
    sumCorrect = +sumCorrect + +correctAnswers[i].value;
    sumWrong = +sumWrong + +wrongAnswers[i].value;
    sumNoChecked = +sumNoChecked + +noCheckedAnswers[i].value;
}
showCount.innerHTML = sumCount;
showCorrect.innerHTML = sumCorrect;
showWrong.innerHTML = sumWrong;
showNoChecked.innerHTML = sumNoChecked;