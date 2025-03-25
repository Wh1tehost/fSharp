% Базовый предикат проверки принадлежности элемента списку
member(X, [X|_]).
member(X, [_|T]) :- member(X, T). % Если данный элемент не совпал с головой списка, то рекурсивно проверяем хвост списка

% Предикат нахождения пересечения двух множеств
cross([], _, []).  % Пересечение с пустым множеством - пустое
cross([X|Xs], Ys, [X|Zs]) :-
    member(X, Ys),        % Если элемент X есть в Y
    cross(Xs, Ys, Zs).
cross([X|Xs], Ys, Zs) :-
    \+ member(X, Ys),     % Если элемента X нет в Y
    cross(Xs, Ys, Zs).

% Основной цикл программы
start :-
    nl,
    write('Программа нахождения пересечения множеств'), nl,
    write('Вводите множества в (в формате [a,b,c].'), nl, nl,
    program_loop.

program_loop :-
    write('> Введите первое множество (или q для завершения): '),
    read(Set1), 
    (Set1 == q -> 
        write('Программа завершена.'), nl, halt
    ;
    write('> Введите второе множество (или q для завершения): '),
    read(Set2),
    (Set2 == q -> 
        write('Программа завершена.'), nl, halt
    ;
    cross(Set1, Set2, Result),
    format('Результат пересечения: ~w~n~n', [Result]),
    program_loop)).

:- initialization(start).