# 
# 
# 
import copy

QUEUE = []
GOAL  = []
move = [[1,-2], [2,-1], [2,1], [1,2], [-1,2], [-2,1], [-2,-1], [-1,-2]]
answer = []

class State:
	posX = "abcdefgh"
	posY = "87654321"
	def __init__(self, board, x, y, step, prev):
		self.board = copy.deepcopy(board)
		self.x = copy.deepcopy(x)
		self.y = copy.deepcopy(y)
		self.board[self.x][self.y] = "x"
		self.step = copy.deepcopy(step)
		self.prev = prev
	
	def paintPos(self):
		return str(self.step) + "(" + str(self.posX[self.x % 8]) + ","+ str(self.posY[self.y % 8]) + ")\n"
	
	def printBoard(self):
		result = ""
		for y in (range(8)):
			result = result + str(self.posY[y]) + " "
			for x in (range(8)):
				if (self.x == x and self.y == y):
					result = result + "K "
				else:
					result = result + self.board[x][y] + " "
			result = result + "\n"
		result = result + "  "
		for s in (self.posX):
			result = result + s + " "
		result = result + "\n"
		return result

def search():
	step = 0
	count = 0
	while not len(QUEUE) == 0:
		st = QUEUE.pop(0)
		if (step <> st.step):
			print step, count
			step = st.step
			count = 0
		# if (2 == st.step):
		# 	st.paintPos()
		# 	st.printBoard()
		for mv in (move):
			x = st.x + mv[0]
			y = st.y + mv[1]
			if (0 <= x < 8 and 0 <= y < 8 and st.board[x][y] == "_"):
				next = State(st.board, x, y, st.step + 1, st)
				if (next.board == GOAL):
					answer.append(next)
					# return
				else:
					QUEUE.append(next)
		step = st.step
		count = count + 1
	
	if (len(answer) <> 0):
		print ""
		print "探索完了:", len(answer)
	else:
		print ""
		print "解答無し"


# 初期化
board = []
for i in (range(8)):
	board.append([])
	GOAL.append([])
	for j in (range(8)):
		board[i].append("_")
		GOAL[i].append("x")

# for i in (range(3)):
# 	for j in (range(4)):
# 		board[i][j] = "_"

x = 0
y = 0
step = 0
start = State(board, x, y, step, None)

QUEUE.append(start)

# 開始
search()

# 結果
for s in (answer):
	text = ""
	st = s
	while st <> None:
		text = st.paintPos() + st.printBoard() + "\n" + text
		st = st.prev
	text = text + "------------------\n\n"
	print text
