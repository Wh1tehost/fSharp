% Основной предикат: проверка упорядоченности списка
ordered([]).            
ordered([_]).          
ordered([X,Y|T]) :-      % Для списка из двух и более элементов
    X =< Y,              
    ordered([Y|T]).      % Рекурсивно проверяем хвост списка

% Основной цикл программы
start :-
    nl,
    write('Программа проверки упорядоченности списка'), nl,
    program_loop.

program_loop :-
    write('Введите список [a,b,c,...] или "q": '),
    flush_output, % Сброс буфера ввода
    read(Input),
    (   Input == q
    ->  write('Программа завершена.'), nl
    ;   (   ordered(Input)
        ->  write('Список упорядочен по возрастанию.'), nl, nl
        ;   write('Список НЕ упорядочен по возрастанию.'), nl, nl
        ),
        program_loop
    ).

:- initialization(start).