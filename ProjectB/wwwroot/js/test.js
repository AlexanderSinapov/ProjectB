// Client-side JavaScript
$(document).ready(function () {
    $('.test-button').click(function () {
        const work = $(this).data('work');
        loadTest(work);
    });

    $('#nextQuestion').click(function () {
        const selectedAnswer = $('input[name="answer"]:checked').val();
        if (selectedAnswer === undefined) {
            alert('Моля, изберете отговор');
            return;
        }
        userAnswers[currentQuestions[currentQuestionIndex].id] = parseInt(selectedAnswer);
        if (currentQuestionIndex < currentQuestions.length - 1) {
            currentQuestionIndex++;
            displayQuestion();
        } else {
            submitTest();
        }
    });
});

function loadTest(work) {
    $.ajax({
        url: '/Test/GetTest',
        method: 'GET',
        data: { work: work },
        success: function (response) {
            currentQuestions = response;
            currentQuestionIndex = 0;
            userAnswers = {};

            const workTitles = {
                'onegin': 'Евгений Онегин',
                'goriot': 'Дядо Горио',
                'bovary': 'Мадам Бовари'
            };

            $('#testTitle').text(workTitles[work]);
            $('#totalQuestions').text(currentQuestions.length);
            $('#questionContainer').show();
            $('#resultsContainer').hide();
            $('#nextQuestion').show();
            displayQuestion();
            $('#testModal').modal('show');
        },
        error: function (error) {
            console.error('Error fetching questions:', error);
            alert('Възникна грешка при зареждането на теста.');
        }
    });
}

function displayQuestion() {
    const question = currentQuestions[currentQuestionIndex];
    $('#currentQuestionNum').text(currentQuestionIndex + 1);
    $('#questionText').text(question.question);

    const optionsHtml = question.options.map((option, index) => `
        <div class="form-check">
            <input class="form-check-input" type="radio" name="answer" 
                   id="option${index}" value="${index}">
            <label class="form-check-label" for="option${index}">
                ${option}
            </label>
        </div>
    `).join('');

    $('#optionsContainer').html(optionsHtml);
    $('input[name="answer"]').prop('checked', false);

    $('#nextQuestion').text(
        currentQuestionIndex === currentQuestions.length - 1 ?
            'Завърши теста' : 'Следващ въпрос'
    );
}

function submitTest() {
    $.ajax({
        url: '/Test/SubmitTest',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(userAnswers),
        success: function (result) {
            $('#questionContainer').hide();
            $('#scoreDisplay').text(result.score);
            $('#resultsContainer').show();
            $('#nextQuestion').hide();
        },
        error: function (error) {
            console.error('Error submitting test:', error);
            alert('Възникна грешка при изпращането на теста.');
        }
    });
}