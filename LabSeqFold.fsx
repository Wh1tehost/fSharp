open System

// Функция проверки первого символа (с учётом отрицательных чисел)
let startDigit digit number =
    let str = number.ToString() 
    let firstIndex = if str.[0] = '-' then 1 else 0  // Определяем индекс первого символа числа
    str.[firstIndex] = digit  // Сравниваем первую цифру

// Функция аккумулятора
let folder digit acc x =
    if startDigit digit x then
        acc + x
    else
        acc 

// Функция для создания последовательности автоматическим вводом
let rec Auto () =
    let random = Random()
    printf "Введите кол-во чисел в последовательности: "
    let N = System.Console.ReadLine()
    printf "Введите нижнюю границу диапазона: "
    let minValue = System.Console.ReadLine()
    printf "Введите верхнюю границу диапазона: "
    let maxValue = System.Console.ReadLine()
    
    match System.Int32.TryParse(N), System.Int32.TryParse(minValue), System.Int32.TryParse(maxValue) with
    | (true, n), (true, minVal), (true, maxVal) when n > 0 && minVal < maxVal -> // Проверка на целочисленность, натуральность для n и правильный порядок для диапазона
        let randSeq = [for _ in 1 .. n -> float (random.Next(minVal, maxVal + 1)) ]  // Генерация последовательности (преобразуем в float)
        randSeq |> Seq.ofList // Возвращаем сгенерированную последовательность
    | _ ->
        printfn "Ошибка ввода. Пожалуйста, введите корректные числа."
        Auto ()  // Повторный запрос

// Функция для создания последовательности ручным вводом
let rec Manual acc =
    printf "Введите число (целое или дробное, положительное или отрицательное) или 'q' для выхода: "
    let input = System.Console.ReadLine()
    
    match input with
    | "q" -> 
        printfn "" 
        acc  // Возвращаем накопленную последовательность
    | _ ->
        match System.Double.TryParse(input) with  // Проверка на число
        | true, number ->  
        // Рекурсивное добавление числа в последовательность
            let newAcc = seq {
                yield! acc
                yield number
            }
            Manual newAcc //Повторный запрос
        | _ ->
            printfn "Ошибка ввода. Пожалуйста, введите число."
            printfn "" 
            Manual acc  // Продолжаем ввод

// Функция для выбора способа заполнения последовательности
let rec inputNumb () = 
    printfn "Введите каким способом заполнить последовательность."
    printfn "1. Ручной ввод"
    printfn "2. Случайная генерация чисел с заданным кол-вом"
    let input = System.Console.ReadLine()
    match input with 
    | "1" ->
        Manual Seq.empty  // Запуск ручного ввода (отправляется пустая последовательность)
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
    | true, digit when digit >= 0 && digit <= 9 -> // Проверка на цифру
        digit.ToString().[0]  // Возвращаем цифру как символ
    | _ ->
        printfn "Ошибка ввода. Пожалуйста, введите цифру от 0 до 9."
        inputDigit ()  // Повторный запрос

// Основная функция 
let main() =
    printfn "Программа для нахождения суммы чисел, начинающихся на заданную цифру."

    // Создание последовательности одним из способов
    let inputSeq = inputNumb ()
    printfn "Исходная последовательность: %A" (inputSeq |> Seq.toList)  // Вывод последдовательности путем преобразования в список

    // Ввод цифры
    let digit = inputDigit ()

    // Используем Seq.fold для вычисления суммы
    let result = Seq.fold (folder digit) 0.0 inputSeq  
    printfn "Сумма чисел, начинающихся на '%c': %.2f" digit result
main()