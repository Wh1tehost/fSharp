open System

// Определение бинарного дерева
type Tree<'T> =
    | Leaf // Пустое дерево
    | Node of 'T * Tree<'T> * Tree<'T> // Узел содержащий значение и двух потомков

// Функция для применения к каждому значению дерева (map)
let rec mapTree f tree =
    match tree with
    | Leaf -> Leaf
    | Node(value, left, right) -> Node(f value, mapTree f left, mapTree f right) // Рекурсивно применяем лямбда функцию к левому и правому поддереву

// Функция для вывода дерева
let printTree tree =
    let rec loop indent tree =
        match tree with
        | Leaf -> ()
        | Node(value, left, right) ->
            printfn "%s%s" indent value // Выводим текущий узел с отступом
            
            // Рекурсивно выводим левое и правое поддеревья с отступами
            loop (indent + "--") left
            loop (indent + "--") right
    
  
    loop "" tree // Вывод с нулевого отступа

// Функция для ввода дерева
let rec inputTree() =
    printfn "Введите строку для узла (или нажмите Enter для создания листа):"
    let input = Console.ReadLine()
    
    if String.IsNullOrEmpty(input) then
        Leaf // Если строка пустая, создаём лист
    else
        // Функция для запроса выбора (создавать ли дочерние узлы)
        let rec Children() =
            printfn "Создать дочерние узлы для '%s'? (1. Да, 2. Нет)" input
            let choice = Console.ReadLine()
            
            match choice with
            | "1" ->
                printfn "Введите левое поддерево для узла '%s':" input
                let left = inputTree()
                printfn "Введите правое поддерево для узла '%s':" input
                let right = inputTree()
                Node(input, left, right) // Создание нового узла
            | "2" ->
                Node(input, Leaf, Leaf) // Узел без дочерних элементов
            | _ ->
                printfn "Ошибка: введите 1 или 2."
                Children() // Повторный запрос выбора
        
        Children()

// Основная функция
let main() =
    printfn "Программа для работы с деревом строк."
    let tree = inputTree()

    printfn "\nИсходное дерево:"
    printTree tree

    let modTree = mapTree (fun str -> str + str) tree // Функция для каждого элемента дерева. Приписывание в конец каждой строки её же

    printfn "\nИзмененное дерево:"
    printTree modTree

// Запуск программы
main()