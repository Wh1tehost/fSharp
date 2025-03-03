open System

// Функция проверки первого символа
let startsWithDigit digit number =
    let str = number.ToString() 
    if str.Length > 0 && str.[0] <> '-' then  // Проверяем, что строка не пустая и не начинается с '-'
        str.[0] = digit  // Сравниваем первую цифру
    else
        false

// Функция для List.fold
let folder digit acc x =
    if startsWithDigit digit x then
        acc + x  // Добавляем элемент к аккумулятору
    else
        acc  // Иначе оставляем аккумулятор без изменений

// Функция для создания списка автоматическим вводом
let rec Auto () =
    let random = Random()
    printf "Введите кол-во чисел в списке: "
    let N = System.Console.ReadLine()
    printf "Введите нижнюю границу диапазона: "
    let minValue = System.Console.ReadLine()
    printf "Введите верхнюю границу диапазона: "
    let maxValue = System.Console.ReadLine()
    
    match System.Int32.TryParse(N), System.Int32.TryParse(minValue), System.Int32.TryParse(maxValue) with
    | (true, n), (true, minVal), (true, maxVal) when n > 0 && minVal < maxVal -> //Проверка на целочисленность, натуральность для N и правильный порядок для диапазона
        let randomList = [ for _ in 1 .. n -> float (random.Next(minVal, maxVal + 1)) ]  // Генерация списка (преобразуем в float)
        printfn "Сгенерированный список: %A" randomList
        randomList  // Возвращаем сгенерированный список
    | _ ->
        printfn "Ошибка ввода. Пожалуйста, введите корректные числа."
        Auto ()  // Повторный запрос

// Функция для создания списка ручным вводом
let rec Manual acc =
    printf "Введите число (целое или дробное, положительное или отрицательное) или 'q' для выхода: "
    let input = System.Console.ReadLine()
    
    match input with
    | "q" -> 
        printfn "Выход из программы."
        printfn "" 
        List.rev acc  // Возвращаем список в правильном порядке (переворачиваем)
    | _ ->
        match System.Double.TryParse(input) with  // Проверка на число
        | true, number ->  
            Manual (number :: acc)  // Рекурсивно добавляем число в список
        | _ ->
            printfn "Ошибка ввода. Пожалуйста, введите число."
            printfn "" 
            Manual acc  // Продолжаем ввод

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

// Функция для ввода цифры от 0 до 9
let rec inputDigit () =
    printf "Введите цифру для нахождения суммы: "
    let input = System.Console.ReadLine()
    match System.Int32.TryParse(input) with // Проверка на целочисленность
    | true, digit when digit >= 0 && digit <= 9 -> // от 0 до 9
        digit.ToString().[0]  // Возвращаем цифру как символ
    | _ ->
        printfn "Ошибка ввода. Пожалуйста, введите цифру от 0 до 9."
        inputDigit ()  // Повторный запрос

// Основная функция 
let main() =
    printfn "Программа для нахождения суммы чисел, начинающихся на заданную цифру."

    // Создание списка одним из способов
    let inputList = inputNumb ()
    printfn "Исходный список: %A" inputList

    // Ввод цифры
    let digit = inputDigit ()

    // Используем List.fold для вычисления суммы
    let result = List.fold (folder digit) 0.0 inputList  
    printfn "Сумма чисел, начинающихся на '%c': %.2f" digit result

// Запуск программы
main()