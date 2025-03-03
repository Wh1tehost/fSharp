open System

// Рекурсивная функция для нахождения суммы цифр числа
let rec findSumDigit number currentSum =
    if number = 0 then // Условие выхода из рекурсии
        currentSum
    else
        let lastDigit = number % 10  
        let newSum =  currentSum + lastDigit  
        findSumDigit (number / 10) newSum  

// Функция для выбора способа заполнения списка
let rec inputNumb () = 
    printfn "Введите каким способом заполнить список."
    printfn "1. Ручной ввод"
    printfn "2. Случайная генерация чисел с заданным кол-вом"
    let input = System.Console.ReadLine()
    match input with 
    | "1" ->
        Manual []  // Запуск ручного ввода
    | "2" ->
        Auto ()  // Запуск автоматической генерации
    | _ -> 
        printfn "Некоректный ввод. Попробуйте еще раз"
        inputNumb ()  // Повторный запрос

// Функция для создания списка автоматическим вводом
and Auto () =
    let random = Random()
    printf "Введите кол-во чисел в списке: "
    let N = System.Console.ReadLine()
    printf "Введите диапазон значений в списке: от 1 до "
    let value = System.Console.ReadLine()
    
    match System.Int32.TryParse(N), System.Int32.TryParse(value) with
    | (true, n), (true, maxValue) when n > 0 && maxValue > 0 ->
        let randomList = [ for _ in 1 .. n -> random.Next(1, maxValue + 1) ]  // Генерация списка
        randomList  // Возвращаем сгенерированный список
    | _ ->
        printfn "Ошибка ввода. Пожалуйста, введите корректные числа."
        Auto ()  // Повторный запрос

// Функция для создания списка ручным вводом
and Manual acc =
    printf "Введите натуральное число или 'q' для выхода: "
    let input = System.Console.ReadLine()
    
    match input with
    | "q" -> 
        printfn "Выход из программы."
        printfn "" 
        List.rev acc  // Возвращаем список в правильном порядке (переворачиваем)
    | _ ->
        match System.Int32.TryParse input with
        | true, number when number > 0 ->  // Проверяем, что число натуральное
            Manual (number :: acc)  // Рекурсивно добавляем число в список
        | _ ->
            printfn "Ошибка ввода. Пожалуйста, введите корректное натуральное число."
            printfn "" 
            Manual acc  // Продолжаем ввод

// Основная функция 
let main() =
    printfn "Программа для формирования списка сумм цифр введенных чисел."
    
    let inputList = inputNumb ()  // Выбор способа заполнения списка
    printfn "Исходный список: %A" inputList
    
    let newList = List.map (fun a -> findSumDigit a 0) inputList  // Для каждого элемента a выполняется функция 
    printfn "Список сумм цифр: %A" newList

// Запуск программы
main()