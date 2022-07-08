# Interval Timer

Helper for tabata training.

## Usage

Configure times and exercises through command line options, rest runs automatically.

### Options

| Name          | Default   | Description                                   |
|:--------------|:----------|:----------------------------------------------|
| -warmup       | 60        | time in seconds for warmup/stretching         |
| -exercise     | 30        | time in seconds for each round                |
| -short-braek  | 10        | time in seconds between rounds                |
| -long-break   | 60        | time in seconds between exercises             |
| -rounds       | 8         | number of rounds per exercise                 |
| -cooldown     | 120       | time in seconds after all exercises are done  |

All other options will be treated as excercise names to use.