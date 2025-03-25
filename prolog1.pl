% Предикат для вычисления суммы цифр числа
sum_digit(0, 0) :- !.
sum_digit(N, Sum) :-
    N > 0,
    Digit is N mod 10,
    Next is N // 10,
    sum_digits(Next, NewSum), % Рекурсивный вызов для оставшейся части числа
    Sum is Digit + NewSum.

% Предикат для подсчета количества шагов до нуля
count_steps(N, Steps) :-
    count_steps(N, 0, Steps). % Инициализация аккумулятора

count_steps(0, Steps, Steps) :- !.
count_steps(N, Acc, Steps) :-
    N > 0,
    sum_digit(N, Sum),
    NewN is N - Sum,
    NewAcc is Acc + 1,
    count_steps(NewN, NewAcc, Steps). % Рекурсивный вызов с новым исходным числом

% Основной цикл программы
start :-
    nl,
    write('Программа вычисления шагов до нуля'), nl,
    program_loop.

program_loop :-
    write('Введите число (или q для выхода): '),
    flush_output, % Сброс буфера ввода
    read_line_to_string(user_input, Input), % Ввод значений в формате строки
    (   Input = "q"
    ->  nl, halt
    ;   atom_number(Input, Number) % Преобразование сроки в число
    ->  count_steps(Number, Steps),
        format('Результат: ~d step(s)~n~n', [Steps]),
        program_loop
    ;   write('Ошибка ввода! Пожалуйста, введите число или "stop".'), nl, nl,
        program_loop
    ).

% Автозапуск программы
:- initialization(start).