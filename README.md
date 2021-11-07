# Introduction 

This project is intended to test C# development skills in the context of Porterson's software development.
Whilst the tasks may not cover anywhere near the full range of skills and technologies required, they are representative of much of the type of work done.
Whilst this often may not involve complex frameworks or technologies, the work is at times challenging, both in terms of functionality, but also in using engineering techniques to manage and simplify the software that is developed, and evolve techniques that make the development process cost efficient and effective.

The competing quality aspects of the code should be prioritised in this order:

* Correctness
* Maintainability
* Simplicity
* Confidence of correctness (testing/type safety/other)
* Reusability
* (Time) Efficiency

Whilst all these things being balanced against productivity.

The exercise contains no tricks.
The object of the exercise is to give you the opportunity to demonstrate your skills in a context that is as 'work like' as possible, this isn't an academic exam.

You are encouraged to implement simple code, based on the requirements you have at that point in time, we are expecting your approach to evolve as the requirments become more complex i.e. the techniques used in the last part of the task may be overengineered for the initial part.

The last task is challenging, there are multiple ways of solving it, some may require some time to implement and get right.

Ideally you should present a working solution, or a prototype that is at least partially working, some code is always nice, but feel free to include a text document that describes how you think this could be solved or the issues involved.

As this is pretend work environment, you are encouraged to ask questions if anything is unclear, and whilst I cannot give you answers (it is a test after all!) I may be able to give hints or clarifications.

The code in the repository is a suggested answer structure, it consists of a single solution with 4 console application projects. 
You can, in addition, have other additional projects in your solution, or a different structure if you like, but each solution should represent the state of your code at that point i.e., don't refactor parts 1 & 2 to use the approach in part 3 retrospectively, each application will be assessed against the requirements given for that application.
The code should be C# and preferably developed in visual studio, and the focus is on the actual processing of the file, rather than aspects like command line processing, or logging, though include any aspect that you think is relevant to satisfying the specification and demonstrating your skills.

The requirements are:
1. develop a console application that transforms ExampleInput.xml to ExampleOutput.xml. In ExampleOutput.xml the book elements are grouped together by author.
1. develop a console application that transforms ExampleInput.xml to ExampleOutput.json.
Here the transformation is basically equivalent to the part i, except the output is in JSON.
1. you should notice that the requirements for both the xml and json transformation are very similar, except for the output format.
Can you write an application where the transformation logic is written once, yet is capable of being used to transform the input into both file formats? 
If you find this too hard, or are short of time, or you find your answers to the earlier parts aren't easy to adapt to achieve this, then simply give a sketch design of how you would try to achieve this.
1. develop (or describe) a console application that transforms ExampleInput.xml to ExampleOutput2.xml. You may find your approach to the previous task cannot easily be applied to this new example, if so consider other alternatives.

You will find the 4 files mentioned above in the solution items.
* ExampleInput.xml - this is an example of the input file that will be used in all 3 tasks.
* ExampleOutput.xml - this is desired output of first and third task.
* ExampleOutput.json - this is desired output of first and second task.
* ExampleOutput2.xml - this is desired output of final task.

# Testing

Your solution should contain some sort of testing to validate that your results are as expected, but *DON'T GET SIDETRACKED* into XML/JSON comparisons, there are libraries that can do this. If you know them, use them, but string comparison is more than adequate for the purposes of this test.

(tip - If using string comparison, you may have to edit the example for whitespace to make the files match, or process the file in the tests to remove whitespace before comparison)

# Getting Started

You should clone this repository to a folder on your machine, and you should be able to open the solution in visual studio.
You require dotnet 5.0 to run the example projects (feel free to downgrade the console apps to a framework you do have if you have to).
You should notice 3 console applications projects, one for each stage:

1. requirement 1 above corresponds to project PortersonToXml.
1. requirement 2 above corresponds to project PortersonToJson.
1. requirement 3 above corresponds to project PortersonToXmlAndJson.
1. requirement 4 above corresponds to project PortersonToXml2.

The intention is we can see the application evolve as new requirements emerge, so don't 'upgrade' an earlier application to use the same approach as a later one, it will probably be overengineered if assessed against the requirement for that task.
The implementation of both 1 & 2 will be and should be simpler than the implementation of application 3 & 4.

# Build and Test

The applications should build in visual studio in the normal manner, and if you run the apps, they should error out with "not implemented" exceptions.

# Contribute

You should not try to push your answer back to this repository, but you should create your own repository somewhere else, or simply return your solution in a zip.

# Notes

As it's an assessment, I am committing to `main`, while I should really commit to a feature bracnh...
