# 
# 
# 

import sys
import string

move = [[1,-2], [2,-1], [2,1], [1,2], [-1,2], [-2,1], [-2,-1], [-1,-2]]
goal = []
board = []
answer = []

count = 0
pattern = 0

def printBoard(posX, posY):
	for y in (range(8)):
		for x in (range(8)):
			print board[x][y],
		print ""
	print ""

def printAnswer():
	sys.stderr.write(str(pattern) + " 探索回数:" + str(count)+"\n")
	print "探索回数:" + str(count)
	print "解答表示"
	for y in (range(8)):
		for x in (range(8)):
			if ([x, y] in answer):
				print string.rjust(str(answer.index([x, y])), 2),
			else:
				print "x ",
		print ""
	print ""

def search(step, board, x, y):
	global count
	count = count + 1
	board[x][y] = 'x'
	answer[step][0] = x
	answer[step][1] = y
	
	# 終了判定
	if (goal == board):
		global pattern
		pattern = pattern + 1
		printAnswer()
		board[x][y] = '_'
		answer[step][0] = None
		answer[step][1] = None
		if (pattern < 10):
			return (0)
		else:
			return (1)
	# 移動
	for mv in (move):
		nextX = x + mv[0]
		nextY = y + mv[1]
		if (0 <= nextX < 8 and 0 <= nextY < 8 and board[nextX][nextY] == "_"):
			if(search(step + 1, board, nextX, nextY)):
				# 終了
				return (1)
	board[x][y] = '_'
	answer[step][0] = None
	answer[step][1] = None
	return (0)

# 初期化
for i in (range(8)):
	board.append([])
	goal.append([])
	for j in (range(8)):
		board[i].append("x")
		goal[i].append("x")
		answer.append([None, None])
answer.append([None, None])

for i in (range(8)):
	for j in (range(8)):
		board[i][j] = '_'
startX = 0
startY = 0

print "問題表示"
printBoard(startX, startY)

search (0, board, startX, startY)

